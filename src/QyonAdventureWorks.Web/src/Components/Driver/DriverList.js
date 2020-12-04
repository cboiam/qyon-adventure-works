import React from "react";
import Axios from "../../Axios";
import DataTable from "react-data-table-component";
import { withRouter, Link } from "react-router-dom";

const columns = [
  { name: "Name", selector: "name" },
  { name: "Gender", selector: "gender" },
  {
    name: "Body avarage temperature",
    selector: "bodyAvarageTemperature",
    sortable: true,
  },
  { name: "Weight", selector: "weight", sortable: true },
  { name: "Height", selector: "height", sortable: true },
  { name: "Avarage time spent", selector: "avarageTimeSpent", sortable: true },
];

class DriverList extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      drivers: null,
    };
  }

  componentDidMount() {
    Axios.get("/drivers").then((response) => {
      this.setState({ drivers: response.data });
    });
  }

  rowClicked = (data) => {
    this.props.history.push(`/drivers/${data.id}`);
  };

  veteranFilterChanged = (e) => {
    const filter = e.target.value;

    if (filter === "both") {
      return;
    }

    Axios.get(`/drivers?veteran=${JSON.parse(filter)}`).then((response) => {
      this.setState({ drivers: response.data });
    });
  };

  render() {
    if (!this.state.drivers) {
      return null;
    }

    return (
      <div className="container mb-5">
        <div className="col-12 d-flex justify-content-between mt-5">
          <h4>Drivers</h4>
          <div className="input-group mb-3 w-25">
            <div className="input-group-prepend">
              <label className="input-group-text" htmlFor="veteranSelect">
                Veteran
              </label>
            </div>
            <select
              className="custom-select"
              id="veteranSelect"
              onChange={this.veteranFilterChanged}
              defaultValue="both"
            >
              <option value="both">
                Both
              </option>
              <option value="false">False</option>
              <option value="true">True</option>
            </select>
          </div>
        </div>
        <DataTable columns={columns} data={this.state.drivers} onRowClicked={this.rowClicked} noHeader />
        <div className="mt-3">
          <Link className="btn btn-dark" to="/drivers/new">Add</Link>
        </div>
      </div>
    );
  }
}

export default withRouter(DriverList);
