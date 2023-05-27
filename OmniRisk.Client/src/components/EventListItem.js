import { Typography } from "@mui/material";
import Box from "@mui/material/Box";
import Rating from "@mui/material/Rating";
import PeopleAltIcon from "@mui/icons-material/PeopleAlt";
import NavigationIcon from "@mui/icons-material/Navigation";
import { observer } from "mobx-react-lite";
import { Button } from "@mui/material";
import { useStore } from "../stores/store";
import { useTheme } from "@mui/material/styles";
import DateRangeIcon from "@mui/icons-material/DateRange";
import MinorCrashIcon from "@mui/icons-material/MinorCrash";
import dayjs from "dayjs";

function EventListItem({ event }) {
  const { appStore } = useStore();
  const theme = useTheme();

  return (
    <Box
      sx={{
        backgroundColor: theme.palette.secondary.main,
        p: 2,
        mt: 3,
        ml: 2,
        border: 15,
        borderColor: theme.palette.secondary.main,
        borderRadius: 3,
        mx: 3,
        display: "flex",
        flexWrap: "wrap",
      }}
    >
      <Typography fontWeight="bold" variant="h5">
        {event.eventType.name}
      </Typography>
      <MinorCrashIcon fontSize="large" />
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          mt: 3,
        }}
      >
        {/* <Button sx={{ color: "red", fontSize: 14 }}>See event details</Button> */}
        <Box
          sx={{
            display: "flex",
            alignItems: "center",
            flexWrap: "wrap",
          }}
        >
          <DateRangeIcon />
          <Typography variant="p">
            Data zdarzenia: {dayjs(event.eventdate).format("MM/DD/YYYY")}
          </Typography>
        </Box>

        <Box
          sx={{
            mx: 3,
            display: "flex",
            alignItems: "center",
            flexWrap: "wrap",
          }}
        >
          <NavigationIcon />
          <Typography variant="p">
            {event.distanceFromCurrentLocation}
            km
          </Typography>
        </Box>
      </Box>
    </Box>
  );
}

export default observer(EventListItem);
