import { makeAutoObservable, runInAction } from "mobx";

export default class AppStore {
  openDrawer = false;
  coordinates = [51.0656512, 17.032684];
  currentLocation = [51.0656512, 17.032684];
  coordinatesSet = false;
  addEventModalOpen = false;
  eventModalOpen = false;
  sateliteView = false;
  addEventFlag = false;

  constructor() {
    makeAutoObservable(this);
  }

  setAddEventFlag = (flag) => {
    runInAction(() => {
      this.setAddCourtFlag = flag;
    });
  };

  setCurrentLocation = (flag) => {
    runInAction(() => {
      this.currentLocation = flag;
    });
  };

  setCoordinatesSet = (flag) => {
    runInAction(() => {
      this.coordinatesSet = flag;
    });
  };

  setOpenDrawer = (flag) => {
    runInAction(() => {
      this.openDrawer = flag;
    });
  };

  setEventModalOpen = (flag) => {
    runInAction(() => {
      this.eventModalOpen = flag;
    });
  };

  setAddEventModalOpen = (flag) => {
    runInAction(() => {
      this.addEventModalOpen = flag;
    });
  };

  setCoordinates = (newCoordinates) => {
    runInAction(() => {
      this.coordinates = newCoordinates;
    });
  };

  setSateliteView = (sateliteView) => {
    runInAction(() => {
      this.sateliteView = sateliteView;
    });
  };
}
