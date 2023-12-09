/* eslint-disable react/no-unescaped-entities */
import React, { useState, useEffect } from "react";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Grid from "@mui/material/Grid";
import Stack from "@mui/material/Stack";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import CameraAltTwoToneIcon from "@mui/icons-material/CameraAltTwoTone";

function Home(props) {
  const [apodData, setApodData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const imageMediaExtensions = ["jpg", "gif"];

  useEffect(() => {
    const fetchApodData = async () => {
      try {
          const response = await fetch(`${import.meta.env.VITE_REACT_APP_SPACEIMAGES_BASE_API}/api/nasaapi/apod`);
        if (!response.ok) {
          throw new Error("Failed to fetch data");
        }

        const data = await response.json();
        setApodData(data);
        setLoading(false);
      } catch (error) {
        setError(error);
        setLoading(false);
      }
    };

    fetchApodData();
  }, []);

  if (loading) {
    return <p>Loading...</p>;
  }

  if (error) {
    return <p>Error: {error.message}</p>;
  }

  return (
    <div>
      <main>
        {/* Hero unit */}
        <Box
          sx={{
            bgcolor: "background.paper",
            pt: 8,
            pb: 6,
          }}
        >
          <Container maxWidth="sm">
            <Typography component="h1" variant="h2" align="center" color="text.primary" gutterBottom>
              Album layout
            </Typography>
            <Typography variant="h6" align="center" color="text.secondary" paragraph>
              Imagine a daily dose of awe-inspiring space images delivered straight to your eyeballs! That's what NASA's Astronomy Picture of the Day (APOD) is all about. It's like a cosmic calendar, showcasing the universe's beauty and vastness through captivating photographs, illustrations, and even videos. 
            </Typography>
            <Stack sx={{ pt: 4 }} direction="row" spacing={2} justifyContent="center">
              <Button variant="contained" href="https://api.nasa.gov/">Explore NASA Open APIs</Button>
              <Button variant="outlined" href="https://github.com/nasa/apod-api">Explore APOD API</Button>
            </Stack>
          </Container>
        </Box>
        <Container sx={{ py: 8 }} maxWidth="md">
          {/* End hero unit */}
          <Grid container spacing={4}>
            {apodData.map((data) => (
              <Grid item key={data.date} xs={12} sm={6} md={4}>
                <Card
                  sx={{
                    height: "100%",
                    display: "flex",
                    flexDirection: "column",
                  }}
                >
                  {imageMediaExtensions.some((ext) => data.url.includes(ext)) ? (
                    <CardMedia
                      sx={{
                        // 16:9
                        pt: "56.25%",
                      }}
                      image={data.url}
                    />
                  ) : (
                    <CardMedia
                      component="iframe"
                      //   sx={{
                      //     // 16:9
                      //     pt: "56.25%",
                      //   }}
                      image={data.url}
                    />
                  )}
                  <CardContent sx={{ flexGrow: 1 }}>
                    <Typography gutterBottom variant="h5" component="h2">
                      {data.title}
                    </Typography>
                    <Typography>{data.explanation}</Typography>
                    {data.copyright ? (
                      <>
                        {/* todo: adjust camera icon to be inline with the text */}
                        <Typography>
                          [<CameraAltTwoToneIcon sx={{}} /> {data.copyright}]
                        </Typography>
                      </>
                    ) : null}
                  </CardContent>
                  <CardActions>
                    <Button size="small">View</Button>
                    {/* <Button size="small">Edit</Button> */}
                  </CardActions>
                </Card>
              </Grid>
            ))}
          </Grid>
        </Container>
      </main>
    </div>
  );
}

export default Home;
