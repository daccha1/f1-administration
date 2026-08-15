import { Link } from "react-router-dom";

const Layout = ({ children }) => {
  return (
    <div className="w-full min-h-screen bg-[#000b17]">
      <div className="min-w-[70vw] max-w-[80vw] m-auto">
        <header className="mb-12">
          <nav className="text-white flex flex-row justify-between pt-8">
            <logo className="font-medium text-2xl">Raceo</logo>
            <ul className="flex flex-row justify-around gap-20 items-center">
              <li className="text-gray-400 hover:text-gray-100 transition hover:duration-200 duration-400">
                <Link to="/">Home</Link>
              </li>

              <li className="text-gray-400 hover:text-gray-100 transition hover:duration-200 duration-400">
                <Link to="/races">Races</Link>
              </li>
              <li className="text-gray-400 hover:text-gray-100 transition hover:duration-200 duration-400">
                <Link to="/users">Users</Link>
              </li>
              <li className="text-gray-400 hover:text-gray-100 transition hover:duration-200 duration-400">
                <Link to="/statistics">Statistics</Link>
              </li>
              <button className="bg-blue-600 min-w-[10rem] min-h-[2rem] cursor-pointer rounded-md hover:bg-blue-700 transition hover:duration-400 duration-400">
                Get a pass!
              </button>
            </ul>
          </nav>
        </header>
        <main className="w-full">{children}</main>
        <footer>Footer</footer>
      </div>
    </div>
  );
};

export default Layout;
