import React from "react";
import { useAuth } from "../../auth/AuthProvider";
import { Carousel } from "react-bootstrap";
import ExampleCarouselImage from "../components/ExampleCarouselImage";
import bg1 from '../../shared/bg1.jpg';
import bg2 from '../../shared/bg2.jpg';
import bg3 from '../../shared/bg3.jpg';
import AboutSection from "../components/AboutSection";

const HomePage = () => {
  return (
    <>
    <Carousel>
      <Carousel.Item>
        <ExampleCarouselImage src={bg1} text="homepage-image.jpg" />
        <Carousel.Caption>
          <h3>First slide label</h3>
          <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <ExampleCarouselImage src={bg2} text="homepage-image.jpg" />
        <Carousel.Caption>
          <h3>Second slide label</h3>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <ExampleCarouselImage src={bg3} text="homepage-image.jpg" />
        <Carousel.Caption>
          <h3>Third slide label</h3>
          <p>
            Praesent commodo cursus magna, vel scelerisque nisl consectetur.
          </p>
        </Carousel.Caption>
      </Carousel.Item>
    </Carousel>
    <AboutSection></AboutSection>
    </>
  );
};

export default HomePage;
