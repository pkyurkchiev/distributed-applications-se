package com.example.carshop.carshop.dto.response;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class ServiceRecordResponseDTO {

    private Long id;
    private String carMake;
    private String carModel;
    private String licensePlate;
    private String employeeFirstName;
    private String employeeLastName;
    private LocalDate serviceDate;
    private String serviceDescription;
    private BigDecimal serviceCost;
}
