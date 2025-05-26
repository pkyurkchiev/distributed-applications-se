package com.example.carshop.carshop.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class ServiceRecordDTO {
    private Long id;
    private Long serviceCarId;
    private Long employeeId;
    private LocalDate serviceDate;
    private String serviceDescription;
    private BigDecimal serviceCost;
}
