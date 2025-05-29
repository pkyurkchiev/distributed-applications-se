package com.example.carshop.carshop.mappers;

import com.example.carshop.carshop.dto.ServiceRecordDTO;
import com.example.carshop.carshop.dto.response.ServiceRecordResponseDTO;
import com.example.carshop.carshop.entities.impl.ServiceRecordEntity;

public class ServiceRecordMapper {

    public static ServiceRecordResponseDTO toResponseDTO(ServiceRecordEntity entity) {
        if (entity == null) return null;

        ServiceRecordResponseDTO dto = new ServiceRecordResponseDTO();
        dto.setId(entity.getId());

        if (entity.getServiceCarId() != null) {
            dto.setCarMake(entity.getServiceCarId().getMake());
            dto.setCarModel(entity.getServiceCarId().getModel());
            dto.setLicensePlate(entity.getServiceCarId().getLicensePlate());
        }

        if (entity.getEmployeeId() != null) {
            dto.setEmployeeFirstName(entity.getEmployeeId().getFirstName());
            dto.setEmployeeLastName(entity.getEmployeeId().getLastName());
        }

        dto.setServiceDate(entity.getServiceDate());
        dto.setServiceDescription(entity.getServiceDescription());
        dto.setServiceCost(entity.getServiceCost());

        return dto;
    }

    public static ServiceRecordDTO toDTO(ServiceRecordEntity entity) {
        if (entity == null) return null;

        ServiceRecordDTO dto = new ServiceRecordDTO();
        dto.setId(entity.getId());
        dto.setServiceCarId(entity.getServiceCarId().getId());
        dto.setEmployeeId(entity.getEmployeeId().getId());
        dto.setServiceDate(entity.getServiceDate());
        dto.setServiceDescription(entity.getServiceDescription());
        dto.setServiceCost(entity.getServiceCost());

        return dto;
    }

    public static ServiceRecordEntity toEntity(ServiceRecordDTO dto) {
        if (dto == null) return null;

        return ServiceRecordEntity.builder()
                .serviceDate(dto.getServiceDate())
                .serviceDescription(dto.getServiceDescription())
                .serviceCost(dto.getServiceCost())
                .build();
    }

    public static void toEntity(ServiceRecordEntity entity, ServiceRecordDTO dto) {
        if (dto == null || entity == null) return;
        entity.setServiceDate(dto.getServiceDate());
        entity.setServiceDescription(dto.getServiceDescription());
        entity.setServiceCost(dto.getServiceCost());
    }
}
