package com.example.carshop.carshop.services;

import com.example.carshop.carshop.dto.CarDTO;
import com.example.carshop.carshop.dto.CarListDTO;
import com.example.carshop.carshop.dto.request.CarRequestListDTO;
import com.example.carshop.carshop.dto.specification.CarSpecification;
import com.example.carshop.carshop.entities.impl.CarEntity;
import com.example.carshop.carshop.entities.impl.CarImageEntity;
import com.example.carshop.carshop.mappers.CarMapper;
import com.example.carshop.carshop.repositories.ICarImageRepository;
import com.example.carshop.carshop.repositories.ICarRepository;
import com.example.carshop.shared.exceptions.carshop.EntityNotFoundException;
import jakarta.transaction.Transactional;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;

import java.util.Base64;
import java.util.List;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class CarService {

    private final ICarRepository carRepository;

    private final ICarImageRepository carImageRepository;

    @Transactional
    public Page<CarListDTO> getAllCars(CarRequestListDTO carRequestListDTO) {
        Sort sort = carRequestListDTO.getAsc() ? Sort.by(carRequestListDTO.getSortBy()).ascending() : Sort.by(carRequestListDTO.getSortBy()).descending();
        Pageable pageable = PageRequest.of(carRequestListDTO.getPage(), carRequestListDTO.getSize(), sort);
        Page<CarEntity> carPage = carRepository.findAll(new CarSpecification(carRequestListDTO.getFilters()), pageable);
        return carPage.map(CarMapper::toListDTO);
    }

    @Transactional
    public List<CarListDTO> getAllCarsNoBody() {
        List<CarEntity> carPage = carRepository.findAll();
        return carPage.stream().map(CarMapper::toListDTO).collect(Collectors.toList());
    }

    @Transactional
    public List<CarListDTO> getAllInStockCarsNoBody() {
        List<CarEntity> carPage = carRepository.findByInStockTrue();
        return carPage.stream().map(CarMapper::toListDTO).collect(Collectors.toList());
    }

    public  CarDTO getCarById(Long id) {
        return carRepository.findById(id)
                .map(CarMapper::toDTO)
                .orElseThrow(() -> new EntityNotFoundException("Car not found with id: " + id, 404));
    }

    @Transactional
    public Long createCar(CarDTO carDTO) {
        CarEntity car = CarMapper.toEntity(carDTO);

        if (carDTO.getImagesBase64() != null && !carDTO.getImagesBase64().isEmpty()) {
            List<CarImageEntity> images = carDTO.getImagesBase64().stream().map(base64 -> {
                CarImageEntity image = new CarImageEntity();
                image.setCar(car);
                image.setData(Base64.getDecoder().decode(base64));
                return image;
            }).collect(Collectors.toList());

            car.setImages(images);
        }

        return carRepository.save(car).getId();
    }

    @Transactional
    public Long updateCar(CarDTO carDTO) {
        CarEntity car = carRepository.findById(carDTO.getId())
                .orElseThrow(() -> new EntityNotFoundException("Car not found with id: " + carDTO.getId(), 404));

        CarMapper.toEntity(car, carDTO);

        carImageRepository.deleteAll(car.getImages());
        car.getImages().clear();

        if (carDTO.getImagesBase64() != null && !carDTO.getImagesBase64().isEmpty()) {
            List<CarImageEntity> images = carDTO.getImagesBase64().stream().map(base64 -> {
                CarImageEntity image = new CarImageEntity();
                image.setCar(car);
                image.setData(Base64.getDecoder().decode(base64));
                return image;
            }).toList();

            car.getImages().addAll(images);
        }


        return carRepository.save(car).getId();
    }


    @Transactional
    public void deleteCarById(Long id) {
        List<CarImageEntity> carImages = carRepository.findById(id)
                .orElseThrow(() -> new EntityNotFoundException("Car not found with id: " + id, 404)).getImages();
        carImageRepository.deleteAll(carImages);
        carRepository.deleteById(id);
    }
}
