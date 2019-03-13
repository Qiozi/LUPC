/*
SQLyog Community Edition- MySQL GUI v6.16 RC2
MySQL - 5.0.24a-community-nt : Database - qstore
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

USE `qstore`;

/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

/*Table structure for table `tb_check_store` */

DROP TABLE IF EXISTS `tb_check_store`;

CREATE TABLE `tb_check_store` (
  `id` int(6) NOT NULL auto_increment,
  `check_regdate` datetime default NULL,
  `comment` varchar(100) default NULL,
  `staff` varchar(30) default NULL,
  `valid_quantity` int(6) default NULL,
  `invalid_quantity` int(6) default NULL,
  `err_quantity` int(6) default NULL,
  `regdate` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tb_check_store` */

/*Table structure for table `tb_check_store_detail` */

DROP TABLE IF EXISTS `tb_check_store_detail`;

CREATE TABLE `tb_check_store_detail` (
  `id` int(6) NOT NULL auto_increment,
  `ParentID` int(6) default NULL,
  `SerialNo` varchar(15) default NULL,
  `p_code` varchar(50) default NULL,
  `p_name` varchar(200) default NULL,
  `p_cost` decimal(10,2) default NULL,
  `flag` int(1) default '0',
  `regdate` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tb_check_store_detail` */

/*Table structure for table `tb_in_invoice` */

DROP TABLE IF EXISTS `tb_in_invoice`;

CREATE TABLE `tb_in_invoice` (
  `id` int(6) NOT NULL auto_increment,
  `input_regdate` datetime default NULL,
  `invoice_code` varchar(50) default NULL,
  `Supplier` varchar(100) default NULL,
  `staff` varchar(30) default NULL,
  `summary` varchar(100) default NULL,
  `note` varchar(100) default NULL,
  `pay_method` varchar(30) default NULL,
  `pay_total` decimal(10,2) default NULL,
  `regdate` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tb_in_invoice` */

/*Table structure for table `tb_in_invoice_product` */

DROP TABLE IF EXISTS `tb_in_invoice_product`;

CREATE TABLE `tb_in_invoice_product` (
  `id` int(6) NOT NULL auto_increment,
  `in_invoice_id` int(6) default NULL,
  `in_invoice_code` varchar(30) default NULL,
  `p_code` varchar(20) default NULL,
  `quantity` int(6) default NULL,
  `cost` decimal(10,2) default NULL,
  `regdate` timestamp NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tb_in_invoice_product` */

/*Table structure for table `tb_out_invoice` */

DROP TABLE IF EXISTS `tb_out_invoice`;

CREATE TABLE `tb_out_invoice` (
  `id` int(6) NOT NULL auto_increment,
  `input_regdate` datetime default NULL,
  `invoice_code` varchar(50) default NULL,
  `staff` varchar(30) default NULL,
  `summary` varchar(100) default NULL,
  `note` varchar(100) default NULL,
  `pay_method` varchar(30) default NULL,
  `pay_total` decimal(10,2) default NULL,
  `SN_Quantity` int(6) default NULL,
  `NumIid` varchar(50) default NULL,
  `Tid` varchar(50) default NULL,
  `Title` varchar(200) default NULL,
  `Price` varchar(10) default NULL,
  `ReceivedPayment` varchar(50) default NULL,
  `ReceiverAddress` varchar(200) default NULL,
  `ReceiverCity` varchar(40) default NULL,
  `ReceiverDistrict` varchar(30) default NULL,
  `ReceiverMobile` varchar(50) default NULL,
  `ReceiverName` varchar(50) default NULL,
  `ReceiverPhone` varchar(50) default NULL,
  `ReceiverState` varchar(30) default NULL,
  `ReceiverZip` varchar(10) default NULL,
  `is_Taobao` tinyint(1) default '0',
  `regdate` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tb_out_invoice` */

/*Table structure for table `tb_out_invoice_product` */

DROP TABLE IF EXISTS `tb_out_invoice_product`;

CREATE TABLE `tb_out_invoice_product` (
  `id` int(6) NOT NULL auto_increment,
  `out_invoice_id` int(6) default NULL,
  `out_invoice_code` varchar(50) character set utf8 default NULL,
  `SerialNO` varchar(15) character set utf8 default NULL,
  `is_return` tinyint(1) default '0',
  `regdate` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `tb_out_invoice_product` */

/*Table structure for table `tb_out_invoice_product_shipping` */

DROP TABLE IF EXISTS `tb_out_invoice_product_shipping`;

CREATE TABLE `tb_out_invoice_product_shipping` (
  `id` int(6) NOT NULL auto_increment,
  `out_invoice_id` int(6) default NULL,
  `out_invoice_code` varchar(50) default NULL,
  `ShippingCode` varchar(20) default NULL,
  `regdate` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tb_out_invoice_product_shipping` */

/*Table structure for table `tb_pay_method` */

DROP TABLE IF EXISTS `tb_pay_method`;

CREATE TABLE `tb_pay_method` (
  `id` int(11) NOT NULL auto_increment,
  `pay_method` varchar(30) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `tb_pay_method` */

/*Table structure for table `tb_product` */

DROP TABLE IF EXISTS `tb_product`;

CREATE TABLE `tb_product` (
  `id` int(6) NOT NULL auto_increment,
  `p_code` varchar(20) default NULL,
  `num_iid` varchar(50) default NULL,
  `p_name` varchar(200) default NULL,
  `p_cate_id` int(6) default NULL,
  `p_price` decimal(10,4) default '0.0000',
  `p_cost` decimal(10,4) default '0.0000',
  `p_quantity` int(6) default '0',
  `p_taobao_url` varchar(200) default NULL,
  `showit` tinyint(1) default '1',
  `regdate` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tb_product` */

/*Table structure for table `tb_product_cate` */

DROP TABLE IF EXISTS `tb_product_cate`;

CREATE TABLE `tb_product_cate` (
  `id` int(6) NOT NULL auto_increment,
  `cate_name` varchar(20) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `tb_product_cate` */

/*Table structure for table `tb_return_history` */

DROP TABLE IF EXISTS `tb_return_history`;

CREATE TABLE `tb_return_history` (
  `id` int(6) NOT NULL auto_increment,
  `SerialNo` varchar(15) default NULL,
  `out_invoice_id` varchar(6) default NULL,
  `out_invoice_code` varchar(50) default NULL,
  `p_code` varchar(15) default NULL,
  `return_regdate` datetime default NULL,
  `comment` varchar(100) default NULL,
  `staff` varchar(30) default NULL,
  `regdate` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `tb_return_history` */

/*Table structure for table `tb_serial_no` */

DROP TABLE IF EXISTS `tb_serial_no`;

CREATE TABLE `tb_serial_no` (
  `id` int(6) NOT NULL auto_increment,
  `SerialNo` varchar(12) default NULL,
  `is_print` tinyint(1) default NULL,
  `regdate` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `tb_serial_no` */

/*Table structure for table `tb_serial_no_and_p_code` */

DROP TABLE IF EXISTS `tb_serial_no_and_p_code`;

CREATE TABLE `tb_serial_no_and_p_code` (
  `id` int(11) NOT NULL auto_increment,
  `SerialNO` varchar(15) default NULL,
  `p_code` varchar(50) default NULL,
  `in_cost` decimal(10,2) default '0.00',
  `in_regdate` datetime default NULL,
  `out_regdate` datetime default NULL,
  `is_order_code` tinyint(1) default NULL,
  `is_return` tinyint(1) default '0',
  `return_regdate` datetime default NULL,
  `regdate` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tb_serial_no_and_p_code` */

insert  into `tb_serial_no_and_p_code`(`id`,`SerialNO`,`p_code`,`in_cost`,`in_regdate`,`out_regdate`,`is_order_code`,`is_return`,`return_regdate`,`regdate`) values (30,'101114010001','23424124234','78.00','2010-11-18 01:22:00','0001-01-01 00:00:00',0,1,'0001-01-01 00:00:00','2010-11-18 01:23:17'),(31,'101114010003','23424124234','78.00','2010-11-18 01:22:00','0001-01-01 00:00:00',0,0,'0001-01-01 00:00:00','2010-11-18 01:23:17'),(32,'101114010005','23424124234','78.00','2010-11-18 01:22:00','0001-01-01 00:00:00',0,0,'0001-01-01 00:00:00','2010-11-18 01:23:17'),(33,'101114010006','23424124234','78.00','2010-11-18 01:22:00','0001-01-01 00:00:00',0,0,'0001-01-01 00:00:00','2010-11-18 01:23:17'),(34,'101114010004','23424124234','78.00','2010-11-18 01:22:00','0001-01-01 00:00:00',0,0,'0001-01-01 00:00:00','2010-11-18 01:23:17'),(35,'101114010002','23424124234','78.00','2010-11-18 01:22:00','0001-01-01 00:00:00',0,1,'0001-01-01 00:00:00','2010-11-18 01:23:17');

/*Table structure for table `tb_trade` */

DROP TABLE IF EXISTS `tb_trade`;

CREATE TABLE `tb_trade` (
  `id` int(6) NOT NULL auto_increment,
  `seller_nick` varchar(50) default NULL,
  `byuer_nick` varchar(50) default NULL,
  `title` varchar(100) default NULL,
  `type` varchar(10) default NULL COMMENT 'fixed 一口价',
  `created` datetime default NULL,
  `iid` varchar(50) default NULL,
  `price` varchar(10) default NULL,
  `pic_path` varchar(200) default NULL,
  `num` varchar(6) default NULL,
  `tid` varchar(20) default NULL,
  `buyer_message` varchar(200) default NULL,
  `shipping_type` varchar(20) default NULL,
  `alipay_no` varchar(30) default NULL,
  `payment` varchar(10) default NULL,
  `discount_fee` varchar(10) default NULL,
  `adjust_fee` varchar(10) default NULL,
  `receiver_name` varchar(30) default NULL,
  `receiver_state` varchar(30) default NULL,
  `receiver_city` varchar(50) default NULL,
  `receiver_district` varchar(30) default NULL,
  `receiver_address` varchar(100) default NULL,
  `receiver_zip` varchar(10) default NULL,
  `receiver_mobile` varchar(30) default NULL,
  `receiver_phone` varchar(30) default NULL,
  `num_iid` varchar(30) default NULL,
  `regdate` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `tb_trade` */

/*Table structure for table `tb_user` */

DROP TABLE IF EXISTS `tb_user`;

CREATE TABLE `tb_user` (
  `id` int(6) NOT NULL auto_increment,
  `user_name` varchar(20) default NULL,
  `user_code` varchar(20) default NULL,
  `phone` varchar(50) default NULL,
  `address` varchar(200) default NULL,
  `comment` varchar(100) default NULL,
  `section` varchar(20) default NULL,
  `regdate` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tb_user` */

insert  into `tb_user`(`id`,`user_name`,`user_code`,`phone`,`address`,`comment`,`section`,`regdate`) values (1,'Qiozi','001','1234',NULL,'dfsfsff','开发部','2010-11-13 12:05:03');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
