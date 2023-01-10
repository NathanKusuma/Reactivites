import axios, { AxiosResponse } from "axios";
import { Activity } from "../models/activity";

const sleep = (delay: number) => {
  return new Promise((resolve) => {
    setTimeout(resolve, delay);
  });
}; //Membuat const delay dengan promise dan set timeout

axios.defaults.baseURL = "http://localhost:5000/api";

axios.interceptors.response.use(async (response) => {
  try {
    await sleep(1000);
    return response;
  } catch (error) {
    console.log(error);
    return await Promise.reject(error);
  }
}); //execute delay n promise dengan axios

const responseBody = <T>(response: AxiosResponse<T>) => response.data; //Passing response data

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}) =>
    axios.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
  del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};
//Membuat akses CRUD

const Activities = {
  list: () => requests.get<Activity[]>("/activities"), //Ini sama dengan http://localhost:5000/api/activities (ditambah dari baseURL)
  details: (id: string) => requests.get<Activity>(`/activities/${id}`),
  create: (activity: Activity) => axios.post<void>("/activities", activity),
  update: (activity: Activity) =>
    axios.put<void>(`/activities/${activity.id}`, activity),
  delete: (id: string) => axios.delete<void>(`/activities/${id}`),
};
//Menggabungkan CRUD dengan response data sehingga dapat dipanggil
//<void> berarti me return nya void

const agent = {
  Activities,
};
//Membuat default export

//Note:
//<T>merupakan parameter yang digunakan untuk menentukan tipe yang akan dipanggil saat penggabungan CRUD dan response data

export default agent;
