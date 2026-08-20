const SeatingZoneCard = () => {


    return (
        <div className="flex flex-row gap-4 justify-between h-[124px] bg-blue-600 rounded-md p-3 hover:bg-blue-500 hover:transition-[200ms] transition-[200ms] cursor-pointer">
            <div className="h-full aspect-square shrink-0 bg-blue-400"></div>
            <div className="flex-1 min-w-0 bg-blue-400"></div>
        </div>
    );
}

export default SeatingZoneCard;