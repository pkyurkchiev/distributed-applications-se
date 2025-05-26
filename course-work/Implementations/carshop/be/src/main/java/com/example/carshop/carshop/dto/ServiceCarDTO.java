package com.example.carshop.carshop.dto;


import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class ServiceCarDTO {

    private Long id;

    @NotBlank(message = "Make is required")
    private String make;

    @NotBlank(message = "Model is required")
    private String model;

    @NotNull(message = "Manufacture year is required")
    private LocalDate manufactureYear;

    @NotBlank(message = "License plate is required")
    private String licensePlate;

    private Long customerId;
}