import React from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import Axios from "../../Axios";
import { withRouter } from "react-router-dom";

const getNewCircuit = () => ({
  description: "",
});

class CircuitForm extends React.Component {
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

  handleOperationError = (error, formikHelpers) => {
    const errors = error?.response?.data?.errors;

    if (errors) {
      Object.keys(errors).forEach((e) => {
        const field = e.split(".").slice(1).join(".");
        errors[e].forEach((err) => {
          formikHelpers.setFieldError(field, err);
        });
      });
    }
  };

  render() {
    const params = this.props.match.params;
    const isUpdating = params && params.id;

    const operation = isUpdating ? "Update" : "Add";
    const initialValues = isUpdating ? this.state.circuit : getNewCircuit();

    if (isUpdating && !this.state.circuit) {
      return null;
    }

    const submit = (values, formikHelpers) => {
      if (isUpdating) {
        Axios.put(`/circuits/${params.id}`, values)
          .then((response) => {
            this.props.history.push(`/circuits/${params.id}`);
          })
          .catch((error) => this.handleOperationError(error, formikHelpers));
        return;
      }

      Axios.post("/circuits", values)
        .then((response) => {
          this.props.history.push(`/circuits/${response.data.id}`);
        })
        .catch((error) => this.handleOperationError(error, formikHelpers));
    };

    return (
      <div className="container mt-5 mb-5">
        <h4>{operation} Circuit</h4>
        <div className="col-md-4 mt-4">
          <Formik initialValues={initialValues} onSubmit={submit}>
            <Form>
              <div className="form-group">
                <label htmlFor="description">Description</label>
                <Field className="form-control" name="description"></Field>
                <div className="text-danger">
                  <ErrorMessage name="description" />
                </div>
              </div>

              <button type="submit" className="btn btn-dark">
                {operation}
              </button>
            </Form>
          </Formik>
        </div>
      </div>
    );
  }
}

export default withRouter(CircuitForm);
