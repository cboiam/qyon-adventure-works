import React from "react";
import { withRouter, Link } from "react-router-dom";
import Axios from "../../Axios";
import RaceHistoryList from "../RaceHistory/RaceHistoryList";

class CircuitDetail extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      circuit: null,
    };
  }

  componentDidMount() {
    const { id } = this.props.match.params;
    Axios.get(`/circuits/${id}`).then((response) => {
      this.setState({ circuit: response.data });
    });
  }

  deleteCircuit = () => {
    const { id } = this.props.match.params;
    Axios.delete(`/circuits/${id}`).then((response) => {
      this.props.history.push("/circuits");
    });
  }

  render() {
    if (!this.state.circuit) {
      return null;
    }

    const { id } = this.props.match.params;

    return (
      <div className="container mt-5 mb-5">
        <h4>Circuit</h4>
        <div className="container mt-3">
          <p>{this.state.circuit.description}</p>
          <RaceHistoryList raceHistories={this.state.circuit.raceHistories} />
          <div>
            <Link className="btn btn-dark mr-2" to={`/circuits/${id}/edit`}>Edit</Link>
            <button className="btn btn-dark" onClick={this.deleteCircuit}>Delete</button>
          </div>
        </div>
      </div>
    );
  }
}

export default withRouter(CircuitDetail);
