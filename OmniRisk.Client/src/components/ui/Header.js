import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import IconButton from "@mui/material/IconButton";
import MenuIcon from "@mui/icons-material/Menu";
import { observer } from "mobx-react-lite";
import HealthAndSafetyIcon from "@mui/icons-material/HealthAndSafety";
import { useStore } from "../../stores/store";

const Header = () => {
  const { appStore } = useStore();

  return (
    <React.Fragment>
      <AppBar position="static" sx={{ mb: 2 }} open={appStore.openDrawer}>
        <Container maxWidth="xl">
          <Toolbar disableGutters>
            <HealthAndSafetyIcon
              sx={{ display: { xs: "none", md: "flex" }, mr: 1 }}
            />
            <Typography
              variant="h6"
              noWrap
              component="a"
              href="/"
              sx={{
                mr: 2,
                display: { xs: "none", md: "flex" },
                fontFamily: "monospace",
                flexGrow: 1,
                fontWeight: 700,
                letterSpacing: ".3rem",
                color: "inherit",
                textDecoration: "none",
              }}
            >
              OMNIRISK
            </Typography>
            <IconButton
              color="inherit"
              aria-label="open drawer"
              edge="end"
              onClick={() => appStore.setOpenDrawer(true)}
              sx={{ ...(appStore.openDrawer && { display: "none" }) }}
            >
              <MenuIcon />
            </IconButton>
          </Toolbar>
        </Container>
      </AppBar>
    </React.Fragment>
  );
};
export default observer(Header);
