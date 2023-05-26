import Header from "./ui/Header";
import React from "react";
import theme from "./ui/Theme";
import Grid from "@mui/material/Grid";
import { GlobalStyles } from "@mui/material";
import Map from "./Map";
import { observer } from "mobx-react-lite";

const OmniRisk = () => {
  return (
    <>
      <Header />
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
    </>
  );
};
export default observer(OmniRisk);
