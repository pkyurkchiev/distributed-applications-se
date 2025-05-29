package com.example.carshop.carshop.mappers;

import com.example.carshop.carshop.dto.ServiceCarDTO;
import com.example.carshop.carshop.entities.impl.CustomerEntity;
import com.example.carshop.carshop.entities.impl.ServiceCarEntity;

public class ServiceCarMapper {

    public static ServiceCarDTO toDTO(ServiceCarEntity entity) {
        if (entity == null) return null;

        ServiceCarDTO dto = new ServiceCarDTO();
        dto.setId(entity.getId());
        dto.setMake(entity.getMake());
        dto.setModel(entity.getModel());
        dto.setManufactureYear(entity.getManufactureYear());
        dto.setLicensePlate(entity.getLicensePlate());

        if (entity.getCustomer() != null) {
            dto.setCustomerId(entity.getCustomer().getId());
        }

        return dto;
    }

    public static ServiceCarEntity toEntity(ServiceCarDTO dto) {
        if (dto == null) return null;

        return ServiceCarEntity.builder()
                .make(dto.getMake())
                .model(dto.getModel())
                .manufactureYear(dto.getManufactureYear())
                .licensePlate(dto.getLicensePlate())
                .build();
    }

    public static void toEntity(ServiceCarEntity entity, ServiceCarDTO dto) {
        if (dto == null || entity == null) return;

        entity.setMake(dto.getMake());
        entity.setModel(dto.getModel());
        entity.setManufactureYear(dto.getManufactureYear());
        entity.setLicensePlate(dto.getLicensePlate());

    }
}
