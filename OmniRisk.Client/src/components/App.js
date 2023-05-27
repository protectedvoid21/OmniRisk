import React from "react";
import { ThemeProvider } from "@mui/material/styles";
import theme from "./ui/Theme";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { observer } from "mobx-react-lite";
import OmniRisk from "./OmniRisk";
import "../styles.css";

function App() {
  return (
    <ThemeProvider theme={theme}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<OmniRisk />}>
            <Route path="*" element={<Navigate to="/" />}></Route>
          </Route>
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default observer(App);
