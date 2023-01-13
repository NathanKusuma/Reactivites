import React from "react";
import ReactDOM from "react-dom/client";
import "./app/layout/styles.css";
import "semantic-ui-css/semantic.min.css";
import App from "./app/layout/App";
import reportWebVitals from "./reportWebVitals";
import { store, StoreContext } from "./app/stores/store";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <StoreContext.Provider value={store}>
    <App />
  </StoreContext.Provider>
);
//storecontext merupakan const dari file store, ini digunakan untuk menggunakan MobX
reportWebVitals();
