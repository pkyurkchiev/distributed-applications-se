import React from "react";

const AboutSection = () => {
  return (
    <section className="container my-5">
      <h2 className="mb-4">About CarShop</h2>

      <div className="row align-items-center mb-4">
        <div className="col-md-6">
          <h5>Our Passion</h5>
          <p>
            We’re dedicated to classic and vintage cars — preserving their legacy
            and sharing their timeless charm with enthusiasts.
          </p>
        </div>
        <div className="col-md-6 text-md-end">
          {/* Empty right side */}
        </div>
      </div>

      <div className="row align-items-center mb-4">
        <div className="col-md-6 order-md-2">
          <h5>What We Offer</h5>
          <p>
            A curated selection of vintage cars for sale and expert maintenance
            services to keep them in peak condition.
          </p>
        </div>
        <div className="col-md-6 order-md-1 text-md-start">
          {/* Empty left side */}
        </div>
      </div>

      <div className="row align-items-center mb-4">
        <div className="col-md-6">
          <h5>Expert Care</h5>
          <p>
            Our skilled technicians specialize in restoration and servicing of
            classic automobiles, ensuring your investment lasts for generations.
          </p>
        </div>
        <div className="col-md-6 text-md-end">
          {/* Empty right side */}
        </div>
      </div>

      <div className="row align-items-center mb-4">
        <div className="col-md-6 order-md-2">
          <h5>Community</h5>
          <p>
            Join a community of passionate car lovers — attend events, share stories,
            and celebrate automotive history with us.
          </p>
        </div>
        <div className="col-md-6 order-md-1 text-md-start">
          {/* Empty left side */}
        </div>
      </div>

      <div className="row align-items-center mb-4">
        <div className="col d-flex justify-content-center">
            <a href="/contact" className="btn btn-primary">
            Contact Us
            </a>
        </div>
        <div className="col-md-6 text-md-end">
          {/* Empty right side */}
        </div>
      </div>

      
    </section>
  );
};

export default AboutSection;
