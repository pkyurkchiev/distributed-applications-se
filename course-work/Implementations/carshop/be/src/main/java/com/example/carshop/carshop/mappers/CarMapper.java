package com.example.carshop.carshop.mappers;

import com.example.carshop.carshop.dto.CarDTO;
import com.example.carshop.carshop.dto.CarListDTO;
import com.example.carshop.carshop.entities.impl.CarEntity;
import com.example.carshop.carshop.entities.impl.CarImageEntity;

import java.util.Base64;
import java.util.List;
import java.util.stream.Collectors;

public class CarMapper {

    public static CarDTO toDTO(CarEntity entity) {
        if (entity == null) return null;

        CarDTO dto = new CarDTO();
        dto.setId(entity.getId());
        dto.setMake(entity.getMake());
        dto.setModel(entity.getModel());
        dto.setManufactureYear(entity.getManufactureYear());
        dto.setPrice(entity.getPrice());
        dto.setInStock(entity.isInStock());

        if (entity.getImages() != null && !entity.getImages().isEmpty()) {
            List<String> base64Images = entity.getImages().stream()
                    .map(image -> Base64.getEncoder().encodeToString(image.getData()))
                    .collect(Collectors.toList());
            dto.setImagesBase64(base64Images);
        }

        return dto;
    }

    public static CarListDTO toListDTO(CarEntity entity) {
        if (entity == null) return null;

        CarListDTO dto = new CarListDTO();
        dto.setId(entity.getId());
        dto.setMake(entity.getMake());
        dto.setModel(entity.getModel());
        dto.setManufactureYear(entity.getManufactureYear());
        dto.setPrice(entity.getPrice());
        dto.setInStock(entity.isInStock());

        if (entity.getImages() != null && !entity.getImages().isEmpty()) {
            CarImageEntity firstImage = entity.getImages().getFirst();
            dto.setImageBase64(Base64.getEncoder().encodeToString(firstImage.getData()));
        } else {
            dto.setImageBase64(null);
        }



        return dto;
    }

    public static CarEntity toEntity(CarDTO dto) {
        if (dto == null) return null;

        CarEntity entity = new CarEntity();
        entity.setId(dto.getId());
        entity.setMake(dto.getMake());
        entity.setModel(dto.getModel());
        entity.setManufactureYear(dto.getManufactureYear());
        entity.setPrice(dto.getPrice());
        entity.setInStock(dto.isInStock());
        return entity;
    }

    public static void toEntity(CarEntity carEntity, CarDTO dto) {
        if (dto == null) return;

        carEntity.setMake(dto.getMake());
        carEntity.setModel(dto.getModel());
        carEntity.setManufactureYear(dto.getManufactureYear());
        carEntity.setPrice(dto.getPrice());
        carEntity.setInStock(dto.isInStock());

    }
}