import { createContext, useContext } from "react";
import activityStore from "./activityStore";
import CommonStore from "./commonStore";
import modalStore from "./modalStore";
import UserStore from "./userStore";

interface Store {
  activityStore: activityStore;
  commonStore: CommonStore;
  userStore: UserStore;
  modalStore: modalStore;
}

export const store: Store = {
  activityStore: new activityStore(), //Membuat object baru untuk menyimpan data dari activityStore
  commonStore: new CommonStore(), //handle error
  userStore: new UserStore(),
  modalStore: new modalStore(),
};

export const StoreContext = createContext(store); //Membuat react context untuk menyimpan setiap komponen secara global

export function useStore() {
  return useContext(StoreContext); //Membuat reactHooks
}
