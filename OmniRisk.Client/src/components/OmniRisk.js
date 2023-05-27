import Header from "./ui/Header";
import React, { useEffect } from "react";
import theme from "./ui/Theme";
import Grid from "@mui/material/Grid";
import { GlobalStyles } from "@mui/material";
import Map from "./Map";
import { observer } from "mobx-react-lite";
import PersistentDrawerRight from "./ui/PersistentDrawerRight";
import axios from "axios";
import { useStore } from "../stores/store";
import { getDistanceBetweenTwoPoints } from "../utils";
import NewEventModal from "./NewEventModal";

const OmniRisk = () => {
  const { appStore } = useStore();

  useEffect(() => {
    axios.get(`https://localhost:7287/Events`).then((response) => {
      let events = response.data;
      events.forEach((event) => {
        event.distanceFromCurrentLocation = getDistanceBetweenTwoPoints(
          {
            latitude: event.latitude,
            longitude: event.longitude,
          },
          {
            latitude: appStore.currentLocation[0],
            longitude: appStore.currentLocation[1],
          }
        );
      });
      appStore.setEvents(events);
    });

    axios.get(`https://localhost:7287/Events/eventType`).then((response) => {
      let events = response.data;
      appStore.setEventTypes(events);
    });
  }, [appStore]);

  return (
    <>
      <Header />
      <NewEventModal />
      <GlobalStyles
        styles={{
          body: { backgroundColor: theme.palette.background.default },
        }}
      />
      <Grid
        container
        direction="column"
        justifyContent="center"
        alignItems="center"
      >
        <Grid
          item
          sx={{
            width: "100vw",
            overflow: "hidden",
          }}
        >
          <Map />
        </Grid>
      </Grid>
      <PersistentDrawerRight />
    </>
  );
};
export default observer(OmniRisk);
