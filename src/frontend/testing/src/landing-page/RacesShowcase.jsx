import RacePreview from "./RacePreview.jsx";
import { useState, useEffect } from "react";

const RacesShowcase = () => {
  const [races, setRaces] = useState([])
  const [locations, setLocations] = useState([])

  useEffect(() => {
    const getRaces = async () => {
      const res = await fetch("http://localhost:5242/Races/get-all");
      const data = await res.json();

      setRaces(data);
    }

    const getLocations = async () => {
      const res = await fetch(`http://localhost:5242/Locations/get-all`);
      const data = await res.json();
      
      setLocations(data);
    }

    getRaces();
    getLocations();
  }, []);


  return (
    <div className="">
      <h3 className="text-white text-3xl mb-5">Races:</h3>
      <div className="flex flex-row flex-wrap justify-between items-center gap-5">
        {races.map((race) => {
          return <RacePreview key={race.id} race={race} location={(locations.filter(l => l.id == race.locationId))[0]}/>
        })}
      </div>
    </div>
  );
};

export default RacesShowcase;
