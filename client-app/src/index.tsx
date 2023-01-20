import React from "react";
import ReactDOM from "react-dom/client";
import "./app/layout/styles.css";
import "semantic-ui-css/semantic.min.css";
import "react-calendar/dist/Calendar.css";
import reportWebVitals from "./reportWebVitals";
import { store, StoreContext } from "./app/stores/store";
import { RouterProvider } from "react-router-dom";
import { router } from "./app/router/Routes";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <StoreContext.Provider value={store}>
    <RouterProvider router={router} />
  </StoreContext.Provider>
);
//storecontext merupakan const dari file store, ini digunakan untuk menggunakan MobX
reportWebVitals();
