// PROPS: parent daje keyeve detetu, svaki key je novi objekat,

import Layout from "./Layout.jsx";
import LandingPage from "./landing-page/RacesShowcase.jsx";
import { Routes, Route } from "react-router-dom";
import RacePreview from "./landing-page/RacePreview.jsx";
import RacesShowcase from "./landing-page/RacesShowcase.jsx";
import LandingPageMain from "./landing-page/LandingPageMain.jsx";
import GetPass from "./get-pass/GetPass.jsx";

const App = () => {
  const race = {
    name: "Monaco",
    circuit: "Circuit de Monaco",
    length: 1.56,
    laps: "67",
  };
  
  return (
    <Layout>
      <Routes>
        <Route path="/" element={<LandingPageMain/>} />
        <Route path="/races" element={<RacesShowcase race={race} />} />
        <Route path="*" element={<h3 className="text-white">Not found</h3>} />
        <Route path="get-pass" element={<GetPass/>}></Route>
      </Routes>
    </Layout>
  );
};

export default App;
