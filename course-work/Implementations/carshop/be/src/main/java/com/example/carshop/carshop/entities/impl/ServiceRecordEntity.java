package com.example.carshop.carshop.entities.impl;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.time.LocalDate;

@Entity(name = "service_records")
@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class ServiceRecordEntity {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;

    @ManyToOne(cascade = {CascadeType.PERSIST, CascadeType.MERGE})
    @JoinColumn(name = "service_cars", referencedColumnName = "id")
    private ServiceCarEntity serviceCarId;

    @ManyToOne(cascade = {CascadeType.PERSIST, CascadeType.MERGE})
    @JoinColumn(name = "employee", referencedColumnName = "id")
    private EmployeeEntity employeeId;

    private LocalDate serviceDate;
    private String serviceDescription;
    private BigDecimal serviceCost;
}
