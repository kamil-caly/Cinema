import { HallDto, SeatDto } from "../pages/Seats/SeatsPageTypes";
import CinemaSeat from "./CinemaSeat";


type CinemaHallProps = {
    hallData?: HallDto;
    onClick: (seatDto: SeatDto) => void;
}

const CinemaHall: React.FC<CinemaHallProps> = (props) => {

    const handleSeatClick = (seatDto: SeatDto) => {
        props.onClick(seatDto);
    };

    const renderSeats = () => {
        if (!props.hallData) return;

        const seats = [];
        const count = Math.sqrt(props.hallData.capacity);
        for (let r = 1; r <= count; r++) {
            const row = [];
            for (let c = 1; c <= count; c++) {
                row.push(
                    <CinemaSeat
                        seatDto={{ row: r, num: c, vip: r === count && c > 5 && c <= count - 5 }}
                        isTaken={props.hallData.seatDtos.some(s => s.row === r && s.num === c)}
                        onClick={handleSeatClick}
                    />
                );
            }
            seats.push(
                <div key={`row-${r}`} className="flex justify-between">
                    {row}
                </div>
            );
        }

        return <div className="flex flex-col justify-between self-center w-[60vw] h-full">{seats}</div>;
    }

    return (
        <div className="bg-cinemaBlack w-2/3 max-h-[90vh] rounded-md flex flex-col">
            <div className="bg-cinemaTextGrayStrong w-2/3 h-7 self-center mb-8"></div>
            {renderSeats()}
        </div>
    );
}

export default CinemaHall;