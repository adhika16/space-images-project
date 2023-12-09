import React, { Component } from "react";
import CssBaseline from "@mui/material/CssBaseline";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { blueGrey } from '@mui/material/colors';
import Navbar from "./components/Navbar";
import Home from "./components/Home";
import Footer from "./components/Footer";

const theme = createTheme({
  palette: {
    primary: {
      main: blueGrey[500]
    },
  },
});

export default class App extends Component {
  render() {
    return (
      <div>
        <ThemeProvider theme={theme}>
          <CssBaseline />
          <Navbar />
          <Home />
          <Footer />
        </ThemeProvider>
      </div>
    );
  }
}
