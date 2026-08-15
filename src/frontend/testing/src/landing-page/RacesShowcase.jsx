import RacePreview from "./RacePreview.jsx";

const RacesShowcase = () => {
  const race = {
    name: "Monaco",
    circuit: "Circuit de Monaco",
    length: 1.56,
    laps: "67",
  };

  return (
    <div className="">
      <h3 className="text-white text-3xl mb-5">Races:</h3>
      <div className="flex flex-row flex-wrap justify-between items-center gap-5">
        <RacePreview race={race}></RacePreview>
        <RacePreview race={race}></RacePreview>
        <RacePreview race={race}></RacePreview>
        <RacePreview race={race}></RacePreview>
        <RacePreview race={race}></RacePreview>
        <RacePreview race={race}></RacePreview>
      </div>
    </div>
  );
};

export default RacesShowcase;
