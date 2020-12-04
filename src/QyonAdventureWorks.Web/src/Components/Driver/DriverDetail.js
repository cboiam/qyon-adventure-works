import React from "react";
import { withRouter, Link } from "react-router-dom";
import Axios from "../../Axios";
import RaceHistoryList from "../RaceHistory/RaceHistoryList";

class DriverDetail extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      driver: null,
    };
  }

  componentDidMount() {
    const { id } = this.props.match.params;
    Axios.get(`/drivers/${id}`).then((response) => {
      this.setState({ driver: response.data });
    });
  }

  deleteDriver = () => {
    const { id } = this.props.match.params;
    Axios.delete(`/drivers/${id}`).then((response) => {
      this.props.history.push("/drivers");
    }).catch(error => console.log(error.response));
  };

  render() {
    if (!this.state.driver) {
      return null;
    }

    const { id } = this.props.match.params;

    return (
      <div className="container mt-5 mb-5">
        <h4>Driver</h4>
        <div className="container mt-3">
          <p>{this.state.driver.name}</p>
          <p>{this.state.driver.gender}</p>
          <p>Weight: {this.state.driver.weight}</p>
          <p>Height: {this.state.driver.height}</p>
          <p>
            Body avarage temperature: {this.state.driver.bodyAvarageTemperature}
          </p>
          <p>Avarage time spent: {this.state.driver.avarageTimeSpent}</p>
          <RaceHistoryList raceHistories={this.state.driver.raceHistories} />
          <div>
            <Link className="btn btn-dark mr-2" to={`/drivers/${id}/edit`}>
              Edit
            </Link>
            <button className="btn btn-dark" onClick={this.deleteDriver}>
              Delete
            </button>
          </div>
        </div>
      </div>
    );
  }
}

export default withRouter(DriverDetail);
