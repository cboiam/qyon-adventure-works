import React from "react";
import DataTable from "react-data-table-component";

const columns = [
  {
    name: "Race date",
    selector: "date",
    sortable: true,
  },
  {
    name: "Time spent",
    selector: "timeSpent",
    sortable: true,
  },
];

export default (props) => {
    console.log(props.raceHistories);
  if (!props.raceHistories || !props.raceHistories.length) {
    return null;
  }

  return (
    <div>
      <h5>Race histories</h5>
      <DataTable columns={columns} data={props.raceHistories} noHeader />
    </div>
  );
};
