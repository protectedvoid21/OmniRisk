import { makeAutoObservable, runInAction } from "mobx";
import { getDistanceBetweenTwoPoints } from "../utils";

export default class AppStore {
  openDrawer = false;
  coordinates = [51.0656512, 17.032684];
  newEventCoordinates = [];
  currentLocation = [51.0656512, 17.032684];
  coordinatesSet = false;
  addEventModalOpen = false;
  eventModalOpen = false;
  sateliteView = false;
  addEventFlag = false;
  events = [];
  eventStatuses = [];
  eventTypes = [];
  loading = false;
  addEventBtnClicked = false;
  persons = [];

  constructor() {
    makeAutoObservable(this);
  }

  setAddEventModalOpen = (flag) => {
    runInAction(() => {
      this.addEventModalOpen = flag;
    });
    var element = document.getElementsByClassName("leaflet-container")[0];
    element.classList.remove("cursor");
  };

  setPersons = (persons) => {
    runInAction(() => {
      this.persons = persons;
    });
  };

  setNewEventCoordinates = (newCoordinates) => {
    runInAction(() => {
      this.newEventCoordinates = newCoordinates;
    });
  };

  setAddEventBtnClicked = (flag) => {
    runInAction(() => {
      this.addEventBtnClicked = flag;
    });
  };

  setLoading = (flag) => {
    runInAction(() => {
      this.loading = flag;
    });
  };

  setEvents = (events) => {
    runInAction(() => {
      this.events = events;
    });
  };

  setEventTypes = (eventTypes) => {
    runInAction(() => {
      this.eventTypes = eventTypes;
    });
  };

  addEvent = (event) => {
    runInAction(() => {
      this.events.push(event);
    });
    var element = document.getElementsByClassName("leaflet-container")[0];
    element.classList.remove("cursor");
  };

  setEventsDistance = (currentLocation) => {
    runInAction(() => {
      let events = this.events;
      events.forEach((event) => {
        event.distanceFromCurrentLocation = getDistanceBetweenTwoPoints(
          {
            latitude: event.latitude,
            longitude: event.longitude,
          },
          {
            latitude: currentLocation[0],
            longitude: currentLocation[1],
          }
        );
      });
      events.sort(
        (a, b) =>
          parseInt(a.distanceFromCurrentLocation) -
          parseInt(b.distanceFromCurrentLocation)
      );
    });
  };

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
    var element = document.getElementsByClassName("leaflet-container")[0];
    element.classList.remove("cursor");
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
