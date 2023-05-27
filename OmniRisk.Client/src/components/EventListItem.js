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
import { CarCrash, Fire, Vandalism, LocalBar, StreetView, Theft, Delete, DriveEta, Security, SportsMma, Pets, Notifications, Power, Opacity, Whatshot, Biohazard, DeleteOutline, Speed, BeachAccess, VolumeUp, Commute, Warning } from '@mui/icons-material';
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
      {getIcon(event.eventType.name)}
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

function getIcon(eventType){
  if (eventType === 'Wypadek samochodowy') {
    return <CarCrash fontSize="large" />;
  }
  if (eventType === 'Podpalenie') {
    return <Fire fontSize="large" />;
  }
  if (eventType === 'Akt wandalizmu') {
    return <Vandalism fontSize="large" />;
  }
  if (eventType === 'Nielegalne spożywanie alkoholu') {
    return <LocalBar fontSize="large" />;
  }
  if (eventType === 'Zamieszki') {
    return <StreetView fontSize="large" />;
  }
  if (eventType === 'Kradzież') {
    return <Theft fontSize="large" />;
  }
  if (eventType === 'Dzikie wysypiska śmieci') {
    return <Delete fontSize="large" />;
  }
  if (eventType === 'Nielegalne rajdy samochodowe') {
    return <DriveEta fontSize="large" />;
  }
  if (eventType === 'Próba oszustwa') {
    return <Security fontSize="large" />;
  }
  if (eventType === 'Przestępstwo na tle seksualnym') {
    return <SportsMma fontSize="large" />;
  }
  if (eventType === 'Chuligaństwo') {
    return <Pets fontSize="large" />;
  }
  if (eventType === 'Groźne zwierzęta') {
    return <Pets fontSize="large" />;
  }
  if (eventType === 'Alert pogodowy') {
    return <Notifications fontSize="large" />;
  }
  if (eventType === 'Przerwa w dostawie prądu') {
    return <Power fontSize="large" />;
  }
  if (eventType === 'Przerwa w dostawie wody') {
    return <Opacity fontSize="large" />;
  }
  if (eventType === 'Przerwa w dostawie gazu') {
    return <Whatshot fontSize="large" />;
  }
  if (eventType === 'Zakażenie biologiczne') {
    return <Biohazard fontSize="large" />;
  }
  if (eventType === 'Nielegalne składowisko odpadów') {
    return <DeleteOutline fontSize="large" />;
  }
  if (eventType === 'Przekroczenie limitu prędkości') {
    return <Speed fontSize="large" />;
  }
  if (eventType === 'Utonięcie') {
    return <BeachAccess fontSize="large" />;
  }
  if (eventType === 'Zakłócenie ciszy nocnej') {
    return <VolumeUp fontSize="large" />;
  }
  if (eventType === 'Zakłócenie działania komunikacji miejskiej') {
    return <Commute fontSize="large" />;
  }
  if (eventType === 'Rozbrajanie bomby') {
    return <Warning fontSize="large" />;
  }

  return null;
}

export default observer(EventListItem);
