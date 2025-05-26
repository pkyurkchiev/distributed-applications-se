package com.example.carshop.carshop.mappers;


import com.example.carshop.carshop.dto.SaleCreateDTO;
import com.example.carshop.carshop.dto.SaleDTO;
import com.example.carshop.carshop.dto.request.SaleRequestDTO;
import com.example.carshop.carshop.dto.response.SaleResponseDTO;
import com.example.carshop.carshop.entities.impl.CarEntity;
import com.example.carshop.carshop.entities.impl.CustomerEntity;
import com.example.carshop.carshop.entities.impl.SaleEntity;

import java.time.LocalDate;

public class SaleMapper {

    public static SaleResponseDTO toResponseDTO(SaleEntity entity) {
        if (entity == null) return null;

        SaleResponseDTO dto = new SaleResponseDTO();
        dto.setSaleId(entity.getId());

        if (entity.getCarId() != null) {
            dto.setCarMake(entity.getCarId().getMake());
            dto.setCarModel(entity.getCarId().getModel());
            dto.setManufactureYear(entity.getCarId().getManufactureYear());
        }

        if (entity.getCustomerId() != null) {
            dto.setCustomerFirstName(entity.getCustomerId().getFirstName());
            dto.setCustomerLastName(entity.getCustomerId().getLastName());
        }

        dto.setSaleDate(entity.getSaleDate());
        dto.setFinalPrice(entity.getFinalPrice());

        return dto;
    }

    public static SaleDTO toDTO(SaleEntity entity) {
        if (entity == null) return null;

        SaleDTO dto = new SaleDTO();
        dto.setId(entity.getId());
        dto.setCarId(entity.getCarId().getId());
        dto.setCustomerId(entity.getCustomerId().getId());
        dto.setSaleDate(entity.getSaleDate());
        dto.setFinalPrice(entity.getFinalPrice());

        return dto;
    }

    public static SaleEntity toEntity(SaleResponseDTO dto) {
        if (dto == null) return null;

        return SaleEntity.builder()
                .saleDate(dto.getSaleDate())
                .finalPrice(dto.getFinalPrice())
                .build();
    }

    public static SaleEntity toEntity(SaleCreateDTO dto) {
        if (dto == null) return null;

        return SaleEntity.builder()
                .saleDate(dto.getSaleDate())
                .finalPrice(dto.getFinalPrice())
                .build();
    }

    public static void toEntity(SaleEntity entity, SaleResponseDTO dto) {
        if (dto == null || entity == null) return;

        entity.setSaleDate(dto.getSaleDate());
        entity.setFinalPrice(dto.getFinalPrice());

        if (entity.getCarId() == null) {
            entity.setCarId(new CarEntity());
        }
        entity.getCarId().setMake(dto.getCarMake());
        entity.getCarId().setModel(dto.getCarModel());
        entity.getCarId().setManufactureYear(dto.getManufactureYear());

        if (entity.getCustomerId() == null) {
            entity.setCustomerId(new CustomerEntity());
        }
        entity.getCustomerId().setFirstName(dto.getCustomerFirstName());
        entity.getCustomerId().setLastName(dto.getCustomerLastName());
    }

    public static void toEntity(SaleEntity entity, SaleCreateDTO dto) {
        if (dto == null || entity == null) return;
        entity.setSaleDate(dto.getSaleDate());
        entity.setFinalPrice(dto.getFinalPrice());
    }
}
