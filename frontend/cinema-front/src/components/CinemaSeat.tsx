import { useState } from "react";
import { SeatDto } from "../pages/Seats/SeatsPageTypes";

type SeatProps = {
    seatDto: SeatDto;
    isTaken: boolean;
    onClick: (seatDto: SeatDto) => void;
}

const CinemaSeat: React.FC<SeatProps> = (props) => {
    const [isSeatSelected, setIsSeatSelected] = useState<boolean>(false);

    const seatClick = () => {
        if (props.isTaken) return;

        setIsSeatSelected(prev => !prev);
        props.onClick(props.seatDto);
    }

    return (
        <div
            key={`row-${props.seatDto?.row}-col-${props.seatDto?.num}`}
            className={`w-6 h-5 flex m-1 text-xs text-cinemaBlack cursor-pointer justify-center items-center
                ${props.isTaken ? 'bg-cinemaTextRed cursor-default' : 'bg-cinemaBgViolet'}
                ${isSeatSelected ? 'bg-cinemaTextGreen' : ''}`}
            onClick={seatClick}
        >
            {`${props.seatDto.vip ? 'VIP' : ''}`}
        </div>
    );
}

export default CinemaSeat;