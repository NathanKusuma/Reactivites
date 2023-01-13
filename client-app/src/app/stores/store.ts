import { createContext, useContext } from "react";
import activityStore from "./activityStore";

interface Store {
  activityStore: activityStore;
}

export const store: Store = {
  activityStore: new activityStore(), //Membuat object baru untuk menyimpan data dari activityStore
};

export const StoreContext = createContext(store); //Membuat react context untuk menyimpan setiap komponen secara global

export function useStore() {
  return useContext(StoreContext); //Membuat reactHooks
}
