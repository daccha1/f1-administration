const RacePreview = ({race}) => {


    return(
        <div className="w-[30rem] h-[28rem] max-w-[30rem] max-h-[30rem] bg-gradient-to-b from-slate-950 via-slate-900 to-blue-950 border-b-4 border-b-blue-900 flex flex-col items-center justify-between pt-15 pb-15 pl-2.5 pr-2.5 border rounded-2xl">
            <div className="w-[60%] flex flex-col items-center">
                <div className="flex flex-row justify-center text-gray-100 text-2xl font-black">{race.name}</div>
                <div className="flex flex-row justify-center text-gray-100 text-2xl font-black">Circuit de Monaco</div>
    
            </div>
            
            <div className="w-[60%] flex flex-col items-center">
                <div className="w-[60%] flex flex-row justify-center text-gray-100 text-md font-black">Length: {race.length} </div>
                <div className="w-[60%] flex flex-row justify-center text-gray-100 text-md font-black">Number of laps: {race.laps}</div>
                <button className="border border-blue-600 text-white min-w-[12rem] min-h-[3rem] cursor-pointer rounded-md hover:bg-blue-700 transition hover:duration-400 duration-400 mt-5">
                Information
              </button>
            </div>
        </div>
    );
}

export default RacePreview;