const Race = async ({ races, setRaces }) => {
  const res = await fetch("https://localhost:7274/Races/get-all");

  const data = await res.json();

  setRaces(data);

  return (
    <ul>
      {data.map((race) => (
        <li>{race.grandPrixName}</li>
      ))}
    </ul>
  );
};

export default Race;
