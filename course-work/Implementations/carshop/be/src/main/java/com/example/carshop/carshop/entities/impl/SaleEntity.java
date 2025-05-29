package com.example.carshop.carshop.entities.impl;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.time.LocalDate;

@Entity(name = "sales")
@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class SaleEntity {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;

    @OneToOne(cascade = {CascadeType.PERSIST, CascadeType.MERGE})
    @JoinColumn(name = "car", referencedColumnName = "id")
    private CarEntity carId;

    @ManyToOne(cascade = {CascadeType.PERSIST, CascadeType.MERGE})
    @JoinColumn(name = "customer", referencedColumnName = "id")
    private CustomerEntity customerId;

    private LocalDate saleDate;
    private BigDecimal finalPrice;
}
