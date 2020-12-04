import "./App.css";
import { BrowserRouter, Route, Switch, NavLink } from "react-router-dom";
import DriverList from "./Components/Driver/DriverList";
import CircuitList from "./Components/Circuit/CircuitList";
import CircuitDetail from "./Components/Circuit/CircuitDetail";
import DriverDetail from "./Components/Driver/DriverDetail";
import CircuitForm from "./Components/Circuit/CircuitForm";
import DriverForm from "./Components/Driver/DriverForm";

function App() {
  return (
    <BrowserRouter>
      <div className="App">
        <header className="App-header">
          <nav className="navbar navbar-expand navbar-dark bg-dark">
            <div className="collapse navbar-collapse">
              <ul className="navbar-nav">
                <li className="nav-item active">
                  <NavLink className="nav-link" to="/" exact>
                    Home
                  </NavLink>
                </li>
                <li className="nav-item">
                  <NavLink className="nav-link" to="/drivers">
                    Drivers
                  </NavLink>
                </li>
                <li className="nav-item">
                  <NavLink className="nav-link" to="/circuits">
                    Circuits
                  </NavLink>
                </li>
              </ul>
            </div>
          </nav>
        </header>

        <Switch>
          <Route path="/drivers/new" component={DriverForm} />
          <Route path="/drivers/:id/edit" component={DriverForm} />
          <Route path="/drivers/:id" component={DriverDetail} />
          <Route path="/drivers" component={DriverList} />
          
          <Route path="/circuits/new" component={CircuitForm} />
          <Route path="/circuits/:id/edit" component={CircuitForm} />
          <Route path="/circuits/:id" component={CircuitDetail} />
          <Route path="/circuits" component={CircuitList} />
        </Switch>
      </div>
    </BrowserRouter>
  );
}

export default App;
