import Box from "@mui/material/Box";
import { Typography, Modal } from "@mui/material";
import Grid from "@mui/material/Grid";
import CloseIcon from "@mui/icons-material/Close";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";
import InputLabel from "@mui/material/InputLabel";
import Select from "@mui/material/Select";
import MenuItem from "@mui/material/MenuItem";
import { useState } from "react";
import FormControl from "@mui/material/FormControl";
import AddAPhotoIcon from "@mui/icons-material/AddAPhoto";
import TextField from "@mui/material/TextField";
import axios from "axios";
import LoadingButton from "@mui/lab/LoadingButton";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "80vw",
  bgcolor: "#D9D9D9",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};

function NewEventModal() {
  const { appStore } = useStore();

  const [loading, setLoading] = useState(false); ///TODO set false
  const [description, setDescription] = useState("");
  const [eventType, setEventType] = useState("");
  const [eventDate, setEventDate] = useState("");

  const handleSubmit = async () => {
    setLoading(true);

    let model = {
      //   authorId: 1,
      eventTypeId: eventType,
      latitude: appStore.newEventCoordinates.lat,
      longitude: appStore.newEventCoordinates.lng,
      description: description,
      eventDate: eventDate,
    };

    console.log(model);
    await axios
      .post(`https://localhost:7287/Events`, model)
      .then((response) => {
        console.log(response);
        setLoading(false);
        appStore.addEvent(response.data);
      });
  };

  return (
    <Modal
      open={appStore.addEventModalOpen}
      onClose={() => {
        appStore.setAddEventModalOpen(false);
        appStore.setAddEventBtnClicked(false);
      }}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <Box
          onClick={() => {
            appStore.setAddEventModalOpen(false);
          }}
          sx={{ position: "absolute", right: 30, cursor: "pointer" }}
        >
          <CloseIcon />
        </Box>
        <Typography variant="h2" align="center">
          Dodaj nowe zdarzenie
        </Typography>
        <Box sx={{ mt: 3 }}>
          <Typography variant="h3" align="left">
            Data zdarzenia
          </Typography>
          <TextField
            id="date"
            label="Birthday"
            type="date"
            value={eventDate}
            sx={{
              width: 200,
            }}
            InputLabelProps={{
              shrink: true,
            }}
            onChange={(event) => setEventDate(event.target.value)}
          />
          <Box>
            <Typography variant="h3" align="left">
              Typ zdarzenia
            </Typography>
            <FormControl sx={{ width: 200 }}>
              <InputLabel id="type-select-label">"Typ zdarzenia</InputLabel>
              <Select
                labelId="type-select-label"
                id="type-select"
                autoWidth
                value={eventType}
                label="Type"
                onChange={(event) => setEventType(event.target.value)}
                sx={{ textAlign: "center" }}
              >
                {appStore.eventTypes.map((type) => (
                  <MenuItem key={type.id} value={type.id}>
                    {type.name}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          </Box>
        </Box>
        <Box sx={{ mt: 3 }}>
          <Typography variant="h3" align="left">
            Opis zdarzenia
          </Typography>
          <Grid item xs={2} sx={{ mr: 5 }}>
            <TextField
              label="Opis zdarzenia"
              value={description}
              onChange={(event) => {
                setDescription(event.target.value);
              }}
            />
          </Grid>
        </Box>
        <Box sx={{ mt: 3 }}></Box>
        <Box textAlign="center" sx={{ mt: 5 }}>
          <LoadingButton
            loading={loading}
            variant="contained"
            size="large"
            color="primary"
            sx={{ width: "60vw", height: 60 }}
            onClick={() => {
              handleSubmit();
              appStore.setAddEventModalOpen(false);
            }}
          >
            SUBMIT COURT
          </LoadingButton>
        </Box>
      </Box>
    </Modal>
  );
}

export default observer(NewEventModal);
