import React from "react";
import { styled, useTheme } from "@mui/material/styles";
import Drawer from "@mui/material/Drawer";
import IconButton from "@mui/material/IconButton";
import ChevronLeftIcon from "@mui/icons-material/ChevronLeft";
import ChevronRightIcon from "@mui/icons-material/ChevronRight";
import { useStore } from "../../stores/store";
import { observer } from "mobx-react-lite";
import { Typography } from "@mui/material";
import Box from "@mui/material/Box";
import useMediaQuery from "@mui/material/useMediaQuery";
import Grid from "@mui/material/Grid";

const DrawerHeader = styled("div")(({ theme }) => ({
  display: "flex",
  alignItems: "center",
  padding: theme.spacing(0, 1),
  // necessary for content to be below app bar
  ...theme.mixins.toolbar,
  justifyContent: "flex-start",
}));

function PersistentDrawerRight() {
  const { appStore } = useStore();
  const theme = useTheme();
  let isXL = useMediaQuery(theme.breakpoints.down("xl"));

  let drawerWidth = isXL ? "32vw" : "24vw";

  const handleDrawerClose = () => {
    appStore.setOpenDrawer(false);
  };

  return (
    <Drawer
      sx={{
        width: drawerWidth,
        flexShrink: 0,
        "& .MuiDrawer-paper": {
          width: drawerWidth,
          backgroundColor: theme.palette.primary.main,
        },
        "& ::-webkit-scrollbar": {
          width: 5,
        },
        "& ::-webkit-scrollbar-track": {
          boxShadow: `inset 0 0 6px rgba(0, 0, 0, 0.3)`,
        },
        "& ::-webkit-scrollbar-thumb": {
          backgroundColor: "darkgrey",
          outline: `1px solid slategrey`,
        },
      }}
      variant="persistent"
      anchor="right"
      open={appStore.openDrawer}
    >
      <DrawerHeader>
        <Box textAlign="center ">
          <IconButton onClick={handleDrawerClose}>
            {theme.direction === "rtl" ? (
              <ChevronLeftIcon />
            ) : (
              <ChevronRightIcon />
            )}
          </IconButton>
        </Box>
      </DrawerHeader>
      <Box textAlign="center">
        <Typography variant="h3">EVENTS NEARBY</Typography>
      </Box>
      <Grid
        container
        direction="column"
        justifyContent="center"
        alignItems="center"
      ></Grid>
    </Drawer>
  );
}

export default observer(PersistentDrawerRight);
