import React from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import Axios from "../../Axios";
import { withRouter } from "react-router-dom";

const getNewDriver = () => ({
  name: "",
  gender: "Male",
  bodyAvarageTemperature: 0,
  weight: 0,
  height: 0,
});

class DriverForm extends React.Component {
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
    const initialValues = isUpdating ? this.state.driver : getNewDriver();

    if (isUpdating && !this.state.driver) {
      return null;
    }

    const submit = (values, formikHelpers) => {
      if (isUpdating) {
        Axios.put(`/drivers/${params.id}`, values)
          .then((response) => {
            this.props.history.push(`/drivers/${params.id}`);
          })
          .catch((error) => this.handleOperationError(error, formikHelpers));
        return;
      }

      Axios.post("/drivers", values)
        .then((response) => {
          this.props.history.push(`/drivers/${response.data.id}`);
        })
        .catch((error) => this.handleOperationError(error, formikHelpers));
    };

    return (
      <div className="container mt-5 mb-5">
        <h4>{operation} Driver</h4>
        <div className="col-md-4 mt-4">
          <Formik initialValues={initialValues} onSubmit={submit}>
            <Form>
              <div className="form-group">
                <label htmlFor="name">Name</label>
                <Field className="form-control" name="name"></Field>
                <div className="text-danger">
                  <ErrorMessage name="name" />
                </div>
              </div>
              
              <div class="form-group">
                <label for="gender">Gender</label>
                <Field as="select" name="gender" className="form-control">
                  <option value="Male">Male</option>
                  <option value="Female">Female</option>
                </Field>
              </div>

              <div className="form-group">
                <label htmlFor="bodyAvarageTemperature">Body avarage temperature</label>
                <Field className="form-control" name="bodyAvarageTemperature" type="number"></Field>
                <div className="text-danger">
                  <ErrorMessage name="bodyAvarageTemperature" />
                </div>
              </div>

              <div className="form-group">
                <label htmlFor="weight">Weight</label>
                <Field className="form-control" name="weight" type="number"></Field>
                <div className="text-danger">
                  <ErrorMessage name="weight" />
                </div>
              </div>

              <div className="form-group">
                <label htmlFor="height">Height</label>
                <Field className="form-control" name="height" type="number"></Field>
                <div className="text-danger">
                  <ErrorMessage name="height" />
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

export default withRouter(DriverForm);
