package com.example.carshop.carshop.services;

import com.example.carshop.carshop.dto.SaleCreateDTO;
import com.example.carshop.carshop.dto.SaleDTO;
import com.example.carshop.carshop.dto.ServiceCarDTO;
import com.example.carshop.carshop.dto.request.SaleRequestDTO;
import com.example.carshop.carshop.dto.request.ServiceCarRequestDTO;
import com.example.carshop.carshop.dto.response.SaleResponseDTO;
import com.example.carshop.carshop.dto.specification.SaleSpecification;
import com.example.carshop.carshop.dto.specification.ServiceCarSpecification;
import com.example.carshop.carshop.entities.impl.CarEntity;
import com.example.carshop.carshop.entities.impl.CustomerEntity;
import com.example.carshop.carshop.entities.impl.SaleEntity;
import com.example.carshop.carshop.entities.impl.ServiceCarEntity;
import com.example.carshop.carshop.mappers.SaleMapper;
import com.example.carshop.carshop.mappers.ServiceCarMapper;
import com.example.carshop.carshop.repositories.ICarRepository;
import com.example.carshop.carshop.repositories.ICustomerRepository;
import com.example.carshop.carshop.repositories.ISaleRepository;
import com.example.carshop.carshop.repositories.IServiceCarRepository;
import com.example.carshop.shared.exceptions.carshop.EntityNotFoundException;
import jakarta.transaction.Transactional;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;

import java.util.Map;

@Service
@RequiredArgsConstructor
public class SaleService {

    private final ISaleRepository saleRepository;

    private final ICustomerRepository customerRepository;

    private final ICarRepository carRepository;

    @Transactional
    public Page<SaleResponseDTO> getAllServiceCars(SaleRequestDTO saleRequestDTO) {

        Sort sort = saleRequestDTO.getAsc() ? Sort.by(saleRequestDTO.getSortBy()).ascending() : Sort.by(saleRequestDTO.getSortBy()).descending();
        Pageable pageable = PageRequest.of(saleRequestDTO.getPage(), saleRequestDTO.getSize(), sort);
        Page<SaleEntity> serviceCarPage = saleRepository.findAll(new SaleSpecification(saleRequestDTO.getFilters()), pageable);
        return serviceCarPage.map(SaleMapper::toResponseDTO);
    }

    public SaleDTO getSaleById(Long id) {
        return saleRepository.findById(id)
                .map(SaleMapper::toDTO)
                .orElseThrow(() -> new EntityNotFoundException("Sale not found with id: " + id, 404));
    }

    @Transactional
    public Long createSale(SaleCreateDTO saleCreateDTO) {
        SaleEntity saleEntity = SaleMapper.toEntity(saleCreateDTO);

        CustomerEntity customer = customerRepository.findById(saleCreateDTO.getCustomerId())
                .orElseThrow(() -> new EntityNotFoundException("Customer not found with ID: " + saleCreateDTO.getCustomerId(), 404));

        CarEntity car = carRepository.findById(saleCreateDTO.getCarId())
                .orElseThrow(() -> new EntityNotFoundException("Car not found with ID: " + saleCreateDTO.getCarId(), 404));

        car.setInStock(false);
        CarEntity carUpdated = carRepository.save(car);

        saleEntity.setCustomerId(customer);
        saleEntity.setCarId(carUpdated);

        return saleRepository.save(saleEntity).getId();
    }

    @Transactional
    public Long updateSale(SaleCreateDTO saleCreateDTO) {
        SaleEntity sale = saleRepository.findById(saleCreateDTO.getId())
                .orElseThrow(() -> new EntityNotFoundException("Sale not found with id: " + saleCreateDTO.getId(), 404));

        CustomerEntity customer = customerRepository.findById(saleCreateDTO.getCustomerId())
                .orElseThrow(() -> new EntityNotFoundException("Customer not found with ID: " + saleCreateDTO.getCustomerId(), 404));

        CarEntity car = carRepository.findById(saleCreateDTO.getCarId())
                .orElseThrow(() -> new EntityNotFoundException("Car not found with ID: " + saleCreateDTO.getCarId(), 404));


        SaleMapper.toEntity(sale, saleCreateDTO);

        sale.setCustomerId(customer);
        sale.setCarId(car);


        return saleRepository.save(sale).getId();
    }

    @Transactional
    public void deleteSale(Long id) {
        SaleEntity sale = saleRepository.findById(id)
                .orElseThrow(() -> new EntityNotFoundException("Sale not found with id: " + id, 404));
        CarEntity car = carRepository.findById(sale.getCarId().getId())
                .orElseThrow(() -> new EntityNotFoundException("Car not found with ID: " + sale.getCarId().getId(), 404));
        car.setInStock(true);
        saleRepository.deleteById(id);
    }

}
