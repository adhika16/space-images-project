import React from "react";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Link from "@mui/material/Link";

function Footer(props) {
  return (
    <div>
      <Box sx={{ bgcolor: "background.paper", p: 6 }} component="footer">
        <Typography variant="h6" align="center" gutterBottom>
          
        </Typography>
        <Typography variant="subtitle1" align="center" color="text.secondary" component="p">
          template by material-ui <a href="https://github.com/mui/material-ui/tree/v5.14.20/docs/data/material/getting-started/templates/album">getting-started templates</a>.
        </Typography>
        <Typography variant="body2" color="text.secondary" align="center">
          {"Copyright "}
          <Link color="inherit" href="">
            Adhika P.
          </Link>{" "}
          {new Date().getFullYear()}
          {"."}
        </Typography>
      </Box>
    </div>
  );
}

export default Footer;
