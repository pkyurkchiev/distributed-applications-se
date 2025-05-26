package com.example.carshop.carshop.dto.response;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class SaleResponseDTO {

    private Long saleId;

    private String carMake;
    private String carModel;
    private LocalDate manufactureYear;

    private String customerFirstName;
    private String customerLastName;

    private LocalDate saleDate;
    private BigDecimal finalPrice;
}