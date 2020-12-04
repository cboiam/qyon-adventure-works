import React from "react";
import Axios from "../../Axios";
import DataTable from "react-data-table-component";
import { withRouter, Link } from "react-router-dom";

const columns = [{ name: "Description", selector: "description" }];

class CircuitList extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      circuits: null,
    };
  }

  componentDidMount() {
    Axios.get("/circuits").then((response) => {
      this.setState({ circuits: response.data });
    });
  }

  rowClicked = (data) => {
    this.props.history.push(`/circuits/${data.id}`);
  };

  usedFilterChanged = (e) => {
    const filter = e.target.value;

    if (filter === "both") {
      return;
    }

    Axios.get(`/circuits?used=${JSON.parse(filter)}`).then((response) => {
      this.setState({ circuits: response.data });
    });
  };

  render() {
    if (!this.state.circuits) {
      return null;
    }

    return (
      <div className="container mb-5">
        <div className="col-12 d-flex justify-content-between mt-5">
          <h4>Circuits</h4>
          <div className="input-group mb-3 w-25">
            <div className="input-group-prepend">
              <label className="input-group-text" htmlFor="usedSelect">
                Used
              </label>
            </div>
            <select
              className="custom-select"
              id="usedSelect"
              onChange={this.usedFilterChanged}
              defaultValue="both"
            >
              <option value="both">Both</option>
              <option value="false">False</option>
              <option value="true">True</option>
            </select>
          </div>
        </div>
        <DataTable
          columns={columns}
          data={this.state.circuits}
          onRowClicked={this.rowClicked}
          noHeader
        />
        <div className="mt-3">
          <Link className="btn btn-dark" to="/circuits/new">Add</Link>
        </div>
      </div>
    );
  }
}

export default withRouter(CircuitList);
