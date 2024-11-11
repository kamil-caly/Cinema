import moment from "moment";
import { SeanceDtoFlat } from "../types/Seance";

interface SeanceCardProps {
    seance: SeanceDtoFlat;
}

const SeanceCard: React.FC<SeanceCardProps> = ({ seance }) => {

    return (
        <div className='flex flex-row p-4'>
            <img
                src={seance.movieImageUrl}
                alt={seance.movieTitle}
                className="object-fill rounded-lg w-24"
            />
            <div className='flex flex-col'>
                <div className='flex flex-col ms-4'>
                    <h2 className="text-2xl font-semibold">{seance.movieTitle}</h2>
                    <p className="text-gray-400 text-sm pt-2">{seance.movieDurationInMin} min</p>
                </div>
                <div className='flex h-full items-center ms-4'>
                    {seance.seanceDates.map((d, i) => (
                        <button className={`bg-yellow-400 text-black hover:bg-black hover:text-yellow-400 min-w-28 p-4 rounded-2xl transition-all duration-600 font-medium text-lg
                            ${i == seance.seanceDates.length - 1 ? '' : 'me-4'}`}>{moment(d).format("HH:mm")}</button>
                    ))}
                </div>
            </div>
        </div>
    );
}

export default SeanceCard;