package com.example.carshop.carshop.entities.impl;


import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;


@Entity(name = "cars")
@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class CarEntity {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;
    private String make;
    private String model;
    private LocalDate manufactureYear;
    private BigDecimal price;
    private boolean isInStock;

    @OneToMany(mappedBy = "car", cascade = CascadeType.ALL, orphanRemoval = true, fetch = FetchType.EAGER)
    private List<CarImageEntity> images = new ArrayList<>();
}
