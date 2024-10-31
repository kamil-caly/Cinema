using CinemaApiDomain.Entities;
using CinemaApiInfrastructure.Persistence;
using CinemaApiDomain.Entities.Enums;

namespace CinemaApiInfrastructure.Seeders
{
    public class CinemaApiSeeder
    {
        private readonly CinemaApiDbContext _dbContext;
        private readonly Random _random;

        public CinemaApiSeeder(CinemaApiDbContext dbContext)
        {
            _dbContext = dbContext;
            _random = new Random();
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (_dbContext.Movies.Any())
                {
                    return;  
                }

                var (movies, halls) = GetDbData();
                await _dbContext.Movies.AddRangeAsync(movies);
                await _dbContext.Halls.AddRangeAsync(halls);
                await _dbContext.SaveChangesAsync();
            }
        }

        private (List<Movie>, List<Hall>) GetDbData()
        {
            var seances = GetSeances();
            var movies = GetMovies();
            var halls = GetHalls();

            for (var i = 0; i < seances.Count; i++)
            {
                movies[_random.Next(movies.Count)].Seances.Add(seances[i]);
                halls[_random.Next(halls.Count)].Seances.Add(seances[i]);
            }

            for (int i = 0; i < 20; i++)
            {
                var randomSeance = seances[_random.Next(seances.Count)];
                var seanceHall = halls.FirstOrDefault(h => h.Seances.Contains(randomSeance));
                if (seanceHall != null)
                {
                    double maxSeatRowNumber = Math.Sqrt(seanceHall.Capacity);
                    var ticket = new Ticket
                    {
                        ReservationCode = Guid.NewGuid().ToString(),
                        Status = TicketState.Valid,
                        PurchaseDate = DateTime.Now,
                        UserEmail = "test@testUser.com",
                        Seats = new List<Seat>
                        {
                            new Seat()
                            {
                                Row = _random.Next((int)maxSeatRowNumber),
                                Number = _random.Next((int)maxSeatRowNumber),
                                VIP = false
                            },
                            new Seat()
                            {
                                Row = _random.Next((int)maxSeatRowNumber),
                                Number = _random.Next((int)maxSeatRowNumber),
                                VIP = false
                            }
                        }
                    };
                    randomSeance.Tickets.Add(ticket);
                }
            }

            return (movies, halls);
        }

        private List<Movie> GetMovies()
        {
            List<string> tittles = new List<string>()
            {
                "Shrek", "Shrek 2", "Shrek 3", "Shrek Forever", "Avatar", "Avatar The Way of Water", "Forrest Gump",
                "The Green Mile", "Joker", "Passengers"
            };

            List<string> descriptions = new List<string>()
            {
                "A humorous fairy tale that follows Shrek, an ogre, on his quest to rescue Princess Fiona and reclaim his swamp, with the help of a talkative donkey.",
                "After Shrek and Fiona's honeymoon, they travel to the kingdom of Far Far Away to meet Fiona's parents, leading to a new adventure filled with hilarity and surprises.",
                "When King Harold falls ill, Shrek is tasked with finding a suitable heir to the throne, all while dealing with his own upcoming fatherhood.",
                "Shrek finds himself longing for his old ogre life, leading him to make a dangerous deal with Rumpelstiltskin that turns his world upside down.",
                "Set on the lush alien world of Pandora, this epic follows Jake Sully, a former Marine, as he navigates his role between two worlds and their cultures.",
                "Returning to Pandora, Jake and Neytiri strive to protect their family amidst new dangers, exploring the depths of Pandora’s oceans.",
                "A heartwarming story of a simple man with a kind heart, whose extraordinary journey leads him through pivotal moments in American history.",
                "A death-row prison guard forms an unlikely friendship with a condemned man with mysterious, otherworldly abilities in this emotional drama.",
                "The dark origin story of Arthur Fleck, a troubled comedian whose spiral into madness ultimately leads him to become the infamous Joker.",
                "A sci-fi romance about two passengers who awaken early from their hibernation on a spaceship, only to find they are stranded with only each other."
            };

            List<int> durationsInMin = new List<int>()
            {
                89, 90, 93, 93, 162, 190, 142, 189, 122, 116
            };

            List<string> ImageUrls = new List<string>()
            {
                "https://static.wikia.nocookie.net/filmopedia/images/3/39/Shrek.jpg/revision/latest?cb=20131115200702&path-prefix=pl",
                "https://www.bilety24.pl/media/cache/repertoire_medium/https---www.bilety24.pl/public/users/235/o/8gadeeb4f9e64didixxwjdmuwfq.jpg",
                "https://fwcdn.pl/fpo/59/73/125973/7535965_2.3.jpg",
                "https://i.ebayimg.com/images/g/1yQAAOSwv8Zdbmwv/s-l1200.jpg",
                "https://m.media-amazon.com/images/I/91N1lG+LBIS._AC_UF1000,1000_QL80_.jpg",
                "https://m.media-amazon.com/images/S/pv-target-images/f0535dd61f56bddd6ee7f3bfb765645e45d78f373418ae37ee5103cf6eebbff0.jpg",
                "https://m.media-amazon.com/images/S/pv-target-images/f9ddd832d1b566f5b8dd29a4dbc76b7531c420c8c8d9fdfe94eca128bda8e2b1.jpg",
                "https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p24429_p_v12_bf.jpg",
                "https://cdn.swiatksiazki.pl/media/catalog/product/6/8/6899906619568.jpg",
                "https://fwcdn.pl/fpo/81/75/558175/7760957_1.3.jpg"
            };

            List<Movie> movies = new List<Movie>();

            for (int i = 0; i < tittles.Count; i++)
            {
                movies.Add(new Movie
                {
                    Title = tittles[i],
                    Description = descriptions[i],
                    DurationInMin = durationsInMin[i],
                    ImageUrl = ImageUrls[i]
                });
            }

            return movies;
        }

        private List<Hall> GetHalls()
        {
            List<string> names = new List<string>()
            {
                "S-A1", "S-A2", "S-A3", "S-B1", "S-B2", "S-B3", "S-C1", "S-C2", "S-C3", "S-D1"
            };

            List<int> capacities = new List<int>()
            {
                784, 784, 784, 625, 625, 625, 484, 484, 484, 400
            };

            List<Hall> halls = new List<Hall>();  
            
            for (int i = 0; i < 10; i++)
            {
                halls.Add(new Hall
                {
                    Name = names[i],
                    Capacity = capacities[i]
                });
            }

            return halls;
        }

        private List<Seance> GetSeances()
        {
            double[] availableHours = {8, 11.5, 15, 18.5, 22};

            List<Seance> seances = new List<Seance>();

            DateTime startDate = DateTime.Today.AddDays(1);
            bool isSkippedSeanceInDay = false;
            double? skippedHourInDay = null;

            for (int i = 0; i < 183; i++)
            {
                DateTime currentDate = startDate.AddDays(i);

                isSkippedSeanceInDay = _random.Next(5) == 0;
                skippedHourInDay = isSkippedSeanceInDay ? availableHours[_random.Next(availableHours.Length)] : null;

                foreach (var hour in availableHours)
                {
                    if (skippedHourInDay == hour)
                    {
                        continue;
                    }
                    DateTime seanceDateTime = currentDate.AddHours(hour);
                    seances.Add(new Seance { Date = seanceDateTime });
                }
            }

            return seances;
        }
    }
}
