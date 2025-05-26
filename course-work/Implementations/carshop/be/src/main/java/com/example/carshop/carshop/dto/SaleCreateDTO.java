package com.example.carshop.carshop.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class SaleCreateDTO {
    private Long id;
    private Long carId;
    private Long customerId;
    private LocalDate saleDate;
    private BigDecimal finalPrice;
}
