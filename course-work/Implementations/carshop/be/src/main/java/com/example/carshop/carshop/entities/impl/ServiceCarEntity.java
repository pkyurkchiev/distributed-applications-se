package com.example.carshop.carshop.entities.impl;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;


import java.time.LocalDate;

@Entity(name = "service_cars")
@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class ServiceCarEntity {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;
    private String make;
    private String model;
    private LocalDate manufactureYear;
    private String licensePlate;

    @ManyToOne
    @JoinColumn(name = "customer_id")
    private CustomerEntity customer;
}
