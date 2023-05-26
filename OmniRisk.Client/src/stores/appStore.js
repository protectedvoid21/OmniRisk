import { makeAutoObservable, runInAction } from "mobx";

export default class AppStore {
  openDrawer = false;

  constructor() {
    makeAutoObservable(this);
  }

  setOpenDrawer = (flag) => {
    runInAction(() => {
      this.openDrawer = flag;
    });
  };
}
