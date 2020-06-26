-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema sistema_venda
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema sistema_venda
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `sistema_venda` DEFAULT CHARACTER SET utf8 ;
USE `sistema_venda` ;

-- -----------------------------------------------------
-- Table `sistema_venda`.`Vendedor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_venda`.`Vendedor` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `senha` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sistema_venda`.`Fornecedor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_venda`.`Fornecedor` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `cnpj` VARCHAR(14) NOT NULL,
  `nome` VARCHAR(45) NOT NULL,
  `contato` VARCHAR(45) NOT NULL,
  `telefone` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sistema_venda`.`Produto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_venda`.`Produto` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NOT NULL,
  `descricao` TEXT NOT NULL,
  `preco_unitario` DECIMAL(9,2) NOT NULL,
  `unidade_medida` CHAR(3) NOT NULL,
  `link_foto` VARCHAR(255) BINARY NOT NULL,
  `Fornecedor_id` INT NOT NULL,
  PRIMARY KEY (`id`, `Fornecedor_id`),
  INDEX `fk_Produto_Fornecedor1_idx` (`Fornecedor_id` ASC),
  CONSTRAINT `fk_Produto_Fornecedor1`
    FOREIGN KEY (`Fornecedor_id`)
    REFERENCES `sistema_venda`.`Fornecedor` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sistema_venda`.`Cliente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_venda`.`Cliente` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NOT NULL,
  `cpf_cnpj` VARCHAR(14) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `senha` VARCHAR(10) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sistema_venda`.`Venda`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_venda`.`Venda` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `data` DATE NOT NULL,
  `total` DECIMAL(9,2) NOT NULL,
  `Vendedor_id` INT NOT NULL,
  `Cliente_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Venda_Vendedor_idx` (`Vendedor_id` ASC),
  INDEX `fk_Venda_Cliente1_idx` (`Cliente_id` ASC),
  CONSTRAINT `fk_Venda_Vendedor`
    FOREIGN KEY (`Vendedor_id`)
    REFERENCES `sistema_venda`.`Vendedor` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Venda_Cliente1`
    FOREIGN KEY (`Cliente_id`)
    REFERENCES `sistema_venda`.`Cliente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sistema_venda`.`Itens_venda`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_venda`.`Itens_venda` (
  `Venda_id` INT NOT NULL,
  `Produto_id` INT NOT NULL,
  `qtde_produto` DECIMAL(9,2) NULL,
  `preco_produto` DECIMAL(9,2) NULL,
  PRIMARY KEY (`Venda_id`, `Produto_id`),
  INDEX `fk_Venda_has_Produto_Produto1_idx` (`Produto_id` ASC),
  INDEX `fk_Venda_has_Produto_Venda1_idx` (`Venda_id` ASC),
  CONSTRAINT `fk_Venda_has_Produto_Venda1`
    FOREIGN KEY (`Venda_id`)
    REFERENCES `sistema_venda`.`Venda` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Venda_has_Produto_Produto1`
    FOREIGN KEY (`Produto_id`)
    REFERENCES `sistema_venda`.`Produto` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sistema_venda`.`Estoque`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_venda`.`Estoque` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `quantidade` FLOAT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sistema_venda`.`Produto_Estoque`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_venda`.`Produto_Estoque` (
  `Produto_id` INT NOT NULL,
  `Estoque_id` INT NOT NULL,
  PRIMARY KEY (`Produto_id`, `Estoque_id`),
  INDEX `fk_Produto_has_Estoque_Estoque1_idx` (`Estoque_id` ASC),
  INDEX `fk_Produto_has_Estoque_Produto1_idx` (`Produto_id` ASC),
  CONSTRAINT `fk_Produto_has_Estoque_Produto1`
    FOREIGN KEY (`Produto_id`)
    REFERENCES `sistema_venda`.`Produto` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Produto_has_Estoque_Estoque1`
    FOREIGN KEY (`Estoque_id`)
    REFERENCES `sistema_venda`.`Estoque` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;