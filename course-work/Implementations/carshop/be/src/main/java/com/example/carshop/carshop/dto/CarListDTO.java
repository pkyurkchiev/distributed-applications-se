package com.example.carshop.carshop.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class CarListDTO {
    private Long id;
    private String make;
    private String model;
    private LocalDate manufactureYear;
    private BigDecimal price;
    private boolean isInStock;

    private String imageBase64;
}