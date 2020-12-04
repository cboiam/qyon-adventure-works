import Axios from "axios";

const instance = Axios.create({ baseURL: `${process.env.REACT_APP_BACKEND_URL}/api/v1` });

export default instance;